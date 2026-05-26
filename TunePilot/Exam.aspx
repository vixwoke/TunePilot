<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Exam.aspx.cs" Inherits="TunePilot.Exam" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Exam</title>
    <link rel="stylesheet" href="css/style.css" />
</head>
<body>
<form id="form1" runat="server">

    <%-- Exam heading --%>
    <h2><asp:Label ID="ExamTitle" runat="server" /></h2>
    <p><asp:Label ID="ExamDescription" runat="server" /></p>
    <p><asp:Label ID="PassingScore" runat="server" /></p>

    <hr />

    <%-- Question dots --%>
    <asp:PlaceHolder ID="QuestionStatusContainer" runat="server" />

    <%-- Question number --%>
    <asp:Label ID="QuestionCounter" runat="server" />

    <hr />

    <%-- Question Instructions --%>
    <h3>Instruction</h3>
    <asp:Label ID="QuestionInstruction" runat="server" />

    <br /><br />

    <%-- Expected Notes --%>
    <asp:HiddenField ID="ExpectedNotesField" runat="server" />

    <%-- Recording Controls --%>
    <button type="button" id="StartRecordingBtn" onclick="startRecording()">Start Recording</button>
    <button type="button" id="StopRecordingBtn" onclick="stopRecording()" disabled>Stop Recording</button>

    <br /><br />
    <asp:Label ID="RecordingStatus" runat="server" Text="Press Start Recording when ready." />

    <br /><br />

    <%-- Detected Notes --%>
    <asp:HiddenField ID="DetectedNotesField" runat="server" />
    <asp:HiddenField ID="AccuracyField" runat="server" />

    <%-- Display --%>
    <p>Detected notes: <asp:Label ID="DetectedNotesDisplay" runat="server" Text="—" /></p>
    <p>Expected notes: <asp:Label ID="ExpectedNotesDisplay" runat="server" Text="—" /></p>
    <p>Accuracy: <asp:Label ID="AccuracyDisplay" runat="server" Text="—" /></p>

    <hr />

    <%-- Navigation --%>
    <asp:Button ID="PrevBtn" runat="server" Text="Prev" OnClick="Prev_Click" />
    <asp:Button ID="NextBtn" runat="server" Text="Next" OnClick="Next_Click" />
    <asp:Button ID="SubmitBtn" runat="server" Text="Submit Exam" OnClick="Submit_Click" Visible="false" />

    <br /><br />
    <asp:Button ID="BackBtn" runat="server" Text="Back to Dashboard" OnClick="Back_Click" />

    <hr />

    <%-- Previous attempts --%>
    <h3>Previous Attempts</h3>
    <asp:PlaceHolder ID="AttemptsContainer" runat="server" />

</form>

<script>
    let audioContext, analyser, microphone, scriptProcessor;
    let isRecording = false;
    let detectedNotes = [];
    let lastNote = null;
    let silenceCounter = 0;

    const NOTE_STRINGS = ['C', 'C#', 'D', 'D#', 'E', 'F', 'F#', 'G', 'G#', 'A', 'A#', 'B'];

    function frequencyToNote(frequency) {
        if (frequency < 20) return null;
        const noteNum = 12 * (Math.log(frequency / 440) / Math.log(2));
        const rounded = Math.round(noteNum) + 69;
        const octave = Math.floor(rounded / 12) - 1;
        const note = NOTE_STRINGS[rounded % 12];
        return note + octave;
    }

    function autoCorrelate(buffer, sampleRate) {
        const SIZE = buffer.length;
        let rms = 0;

        for (let i = 0; i < SIZE; i++) rms += buffer[i] * buffer[i];
        rms = Math.sqrt(rms / SIZE);
        if (rms < 0.01) return -1;

        let r1 = 0, r2 = SIZE - 1;
        const thres = 0.2;

        for (let i = 0; i < SIZE / 2; i++) {
            if (Math.abs(buffer[i]) < thres) { r1 = i; break; }
        }
        for (let i = 1; i < SIZE / 2; i++) {
            if (Math.abs(buffer[SIZE - i]) < thres) { r2 = SIZE - i; break; }
        }

        const buf2 = buffer.slice(r1, r2);
        const c = new Array(buf2.length).fill(0);

        for (let i = 0; i < buf2.length; i++)
            for (let j = 0; j < buf2.length - i; j++)
                c[i] += buf2[j] * buf2[j + i];

        let d = 0;
        while (c[d] > c[d + 1]) d++;

        let maxval = -1, maxpos = -1;
        for (let i = d; i < buf2.length; i++) {
            if (c[i] > maxval) { maxval = c[i]; maxpos = i; }
        }

        let T0 = maxpos;
        const x1 = c[T0 - 1], x2 = c[T0], x3 = c[T0 + 1];
        const a = (x1 + x3 - 2 * x2) / 2;
        const b = (x3 - x1) / 2;

        if (a) T0 = T0 - b / (2 * a);
        return sampleRate / T0;
    }

    function notesMatch(detected, expected) {
        const noteOrder = ['C', 'C#', 'D', 'D#', 'E', 'F', 'F#', 'G', 'G#', 'A', 'A#', 'B'];
        const detectedBase = detected.slice(0, -1);
        const expectedBase = expected.slice(0, -1);
        const detectedOctave = parseInt(detected.slice(-1));
        const expectedOctave = parseInt(expected.slice(-1));
        const detectedIdx = noteOrder.indexOf(detectedBase) + detectedOctave * 12;
        const expectedIdx = noteOrder.indexOf(expectedBase) + expectedOctave * 12;
        return Math.abs(detectedIdx - expectedIdx) <= 1;
    }

    function calculateAccuracy(detected, expectedStr) {
        const expected = expectedStr.trim().split(' ');
        if (detected.length === 0 || expected.length === 0) return 0;

        let matched = 0;
        let detectedIdx = 0;

        for (let i = 0; i < expected.length; i++) {
            while (detectedIdx < detected.length) {
                if (notesMatch(detected[detectedIdx], expected[i])) {
                    matched++;
                    detectedIdx++;
                    break;
                }
                detectedIdx++;
            }
        }

        return Math.round((matched / expected.length) * 100);
    }

    function startRecording() {
        navigator.mediaDevices.getUserMedia({ audio: true }).then(function (stream) {
            audioContext = new (window.AudioContext || window.webkitAudioContext)();
            analyser = audioContext.createAnalyser();
            microphone = audioContext.createMediaStreamSource(stream);
            scriptProcessor = audioContext.createScriptProcessor(2048, 1, 1);

            analyser.fftSize = 2048;
            microphone.connect(analyser);
            analyser.connect(scriptProcessor);
            scriptProcessor.connect(audioContext.destination);

            detectedNotes = [];
            lastNote = null;
            silenceCounter = 0;
            isRecording = true;

            document.getElementById('StartRecordingBtn').disabled = true;
            document.getElementById('StopRecordingBtn').disabled = false;
            document.getElementById('<%= RecordingStatus.ClientID %>').innerText = 'Recording...';
            document.getElementById('<%= DetectedNotesDisplay.ClientID %>').innerText = '—';

            scriptProcessor.onaudioprocess = function (e) {
                if (!isRecording) return;

                const buffer = e.inputBuffer.getChannelData(0);
                const frequency = autoCorrelate(buffer, audioContext.sampleRate);

                if (frequency === -1) {
                    silenceCounter++;
                    if (silenceCounter > 5) lastNote = null;
                    return;
                }

                silenceCounter = 0;
                const note = frequencyToNote(frequency);
                if (!note) return;

                if (note !== lastNote) {
                    detectedNotes.push(note);
                    lastNote = note;
                    document.getElementById('<%= DetectedNotesDisplay.ClientID %>').innerText = detectedNotes.join(' ');
                }
            };

        }).catch(function (err) {
            alert('Microphone access denied: ' + err.message);
        });
    }

    function stopRecording() {
        isRecording = false;

        if (scriptProcessor) scriptProcessor.disconnect();
        if (analyser) analyser.disconnect();
        if (microphone) microphone.disconnect();
        if (audioContext) audioContext.close();

        document.getElementById('StartRecordingBtn').disabled = false;
        document.getElementById('StopRecordingBtn').disabled = true;
        document.getElementById('<%= RecordingStatus.ClientID %>').innerText = 'Recording stopped.';

        const expectedStr = document.getElementById('<%= ExpectedNotesField.ClientID %>').value;
        const accuracy    = calculateAccuracy(detectedNotes, expectedStr);

        document.getElementById('<%= DetectedNotesField.ClientID %>').value = detectedNotes.join(' ');
        document.getElementById('<%= AccuracyField.ClientID %>').value       = accuracy.toString();
        document.getElementById('<%= AccuracyDisplay.ClientID %>').innerText = accuracy + '%';
    }
</script>

</body>
</html>