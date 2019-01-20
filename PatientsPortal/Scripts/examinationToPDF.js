document.getElementById("toPdf").onclick = function fun() {
    var element = document.getElementById('examinationDetails');
    html2pdf(element);
}