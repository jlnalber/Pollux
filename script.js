function showPdfHelp(){
    var pdfFrame = document.createElement("iframe");
    pdfFrame.src = "Hilfe.html";
    var content = document.getElementById("content");
    content.innerHTML = "";
    content.appendChild(pdfFrame);
}