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

function CheckCode(code) {
    $.ajax({
        type: "GET",
        url: "/Wx/CheckQrCode?code=" + code,
        dataType: "json",
        timeout: 3000,
        success: function (data) {
            alert(data.result);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.status);
            alert(XMLHttpRequest.readyState);
            alert(textStatus);
        }
    });
}