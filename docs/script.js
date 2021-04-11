function showPdfHelp(){
    var pdfFrame = document.createElement("iframe");
    pdfFrame.src = "Hilfe.html";
    var content = document.getElementById("content");
    content.innerHTML = "";
    content.appendChild(pdfFrame);
}

function showAbout(){
    var aboutSection = document.createElement("iframe");
    aboutSection.src = "About.html";
    var content = document.getElementById("content");
    content.innerHTML = "";
    content.appendChild(aboutSection);
}

/*function showDownload(){
    var downloadSection = document.createElement("iframe");
    downloadSection.src = "Download.html";
    var content = document.getElementById("content");
    content.innerHTML = "";
    content.appendChild(downloadSection);
}*/

function showPollux(){
    var polluxSection = document.createElement("iframe");
    polluxSection.src = "Pollux.html";
    var content = document.getElementById("content");
    content.innerHTML = "";
    content.appendChild(polluxSection);
}