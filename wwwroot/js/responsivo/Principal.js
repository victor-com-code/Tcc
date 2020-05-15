/*function ChamadaAjax(url, dados, tipo, mycallback) {
    $.ajax({
        url: url,
        data: dados,
        type: tipo,
        contentType: 'application/json',
        success: mycallback
    });
}*/

function ChamadaAjax(url, dados, tipo, mycallback) {
    document.getElementById("carregando").style.display = "block";
    $.ajax({
        url: url,
        data: dados,
        type: tipo,
        contentType: 'application/json',
        success: function (data) {
            document.getElementById("carregando").style.display = "none";
            mycallback(data);
        }
    });
}
function proximoCampoLibrary(id, e) {
    var tecla = (e.keyCode ? e.keyCode : e.which);
    if (tecla == 13) {
        document.getElementById(id).focus();
        return false;
    }
}