wx.ready(function () {
    $("#scanBtn").removeClass("disabled");
});

function scan() {
    wx.scanQRCode({
        needResult: 1, // 默认为0，扫描结果由微信处理，1则直接返回扫描结果，
        scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是一维码，默认二者都有
        success: function (res) {
            var result = res.resultStr; // 当needResult 为 1 时，扫码返回的结果
            CheckCode(result);
        }
    });
}

function show(info) {
    $('#info').html(info);
    $('#dialog_ok').show().on('click', '.weui_btn_dialog', function () {
        $('#dialog_ok').off('click').hide();
        window.opener = null;
        window.open('', '_self');
        window.close();
    });
}

function CheckCode(code) {
    $.ajax({
        type: "GET",
        url: "/Wx/CheckQrCode?code=" + code,
        dataType: "json",
        timeout: 3000,
        success: function (data) {
            if (data.result == "true") {
                show("签到成功");
            } else {
                show("签到失败");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            show(XMLHttpRequest.status + "<br/>" + XMLHttpRequest.readyState + "<br/>" + textStatus);
        }
    });
}