function checkFile() {

    var checkFile = document.getElementById('uploadFile');

    if (checkFile.getAttribute('type') == 'file' && checkFile.files.length > 0) {
        var fName = checkFile.value;
        var fExt = fName.split('.')[fName.split('.').length - 1];
    }

    if (fExt != "srt") {
        document.getElementById('uploadError').classList.remove('hidden');
        document.getElementById('uploadSubmit').classList.add('hidden');
    } else {
        document.getElementById('uploadError').classList.add('hidden');
        document.getElementById('uploadSubmit').classList.remove('hidden');
    }
}