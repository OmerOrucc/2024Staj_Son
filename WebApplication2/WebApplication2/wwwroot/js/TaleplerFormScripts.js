function serversFormPostGenelSekreterKaydet() {

    $("#ddlTalplerFormBirim").val("59206153");

    var frm = $('#frmServers');
    $.ajax({
        type: "POST",
        url: "/Demands/TalepSave",
        data: frm.serialize(),
        success: function (response) {
            SaveControl(response);

        }
    });
}
function serversFormPostKaydet() {
    var frm = $('#frmServers');
    $.ajax({
        type: "POST",
        url: "/Demands/TalepSave",
        data: frm.serialize(),
        success: function (response) {
            SaveControl(response);

        }
    });
}
function serversFormPostOnay() {
    var frm = $('#frmServers');
    $.ajax({
        type: "POST",
        url: "/Demands/TalepOnayla",
        data: frm.serialize(),
        success: function (response) {
            SaveControl(response);
        }
    });
}
function serversFormPostRed() {
    var frm = $('#frmServers');
    $.ajax({
        type: "POST",
        url: "/Demands/TalepReddet",
        data: frm.serialize(),
        success: function (response) {
            SaveControl(response);
        }
    });
}
function KurumDisi(TalepBirim) {
    if (document.getElementById('cbxKurumDisi').checked) {
        document.getElementById('ddlTalplerFormBirim').style.visibility = "hidden";
        document.getElementById('ddlDisBirim').style.visibility = "visible";
    }
    else {
        document.getElementById('ddlTalplerFormBirim').style.visibility = "visible";
        document.getElementById('ddlDisBirim').style.visibility = "hidden";
        $("#ddlBirim").val(TalepBirim);

    }
}