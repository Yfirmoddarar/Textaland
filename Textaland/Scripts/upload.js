function checkFile(f) {
    //checking file extension for .srt
    var fileName = f.value;
    var fileExt = fileName.substr(fileName.lastIndexOf('.') + 1);


    //ifwrong extension show alert message and remove submit button
    if (fileExt != "srt") {
        document.getElementById("uploadError").classList.remove("hidden");
        document.getElementById("upload").classList.add("hidden");
    //if file extension is ok remove the error message and show the submit button
    } else {
        document.getElementById("upload").classList.remove("hidden");
        document.getElementById("uploadError").classList.add("hidden");
    }
}

function loading() {
    document.getElementById("uploadForm").classList.add("hidden");
    document.getElementById("loading").classList.remove("hidden");
}