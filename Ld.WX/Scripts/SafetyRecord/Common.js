if (Sys.MicroMessenger) {
    $('#myModal').modal('show');
    $('#sheet').hide();
}
else if (Sys.chrome) {
    $('#myModal').modal('hide');
    $('#sheet').show();
}

$('.btn-checkbox').buttonCheckBox();

var canvas = document.createElement("canvas");
context = canvas.getContext('2d');

var getBlobBydataURI = function (dataURI, type) {
    var datas = dataURI.split(',');
    var binary = atob(datas.length > 1 ? datas[1] : datas[0]);
    var array = [];
    for (var i = 0; i < binary.length; i++) {
        array.push(binary.charCodeAt(i));
    }
    return new Blob([new Uint8Array(array)], { type: type });
}

wx.config({
    // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，
    // 可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
    debug: false,
    appId: appId, // 必填，公众号的唯一标识
    timestamp: timestamp, // 必填，生成签名的时间戳
    nonceStr: nonceStr, // 必填，生成签名的随机串
    signature: signature,// 必填，签名，见附录1
    jsApiList: ['scanQRCode', 'chooseImage', 'uploadImage', 'getLocalImgData'] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
});

wx.ready(function () {
    // config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后，config是一个客户端的异步操作，
    // 所以如果需要在页面加载时就调用相关接口，则须把相关接口放在ready函数中调用来确保正确执行。对于用户触发时才调用的接口，
    // 则可以直接调用，不需要放在ready函数中。
    $('#myModal').modal('hide');
    $('#sheet').show();
    $('#addImg').on('click', _ => {
        wx.chooseImage({
            count: 1, // 默认9
            sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
            sourceType: ['album', 'camera'], // 可以指定来源是相册还是相机，默认二者都有
            success: function (res) {
                var localIds = res.localIds; // 返回选定照片的本地ID列表，localId可以作为img标签的src属性显示图片

                //alert(res.localIds[0]);
                for (var i = 0; i < localIds.length; i++) {
                    wx.uploadImage({
                        localId: localIds[i], // 需要上传的图片的本地ID，由chooseImage接口获得
                        isShowProgressTips: 1, // 默认为1，显示进度提示
                        success: function (res) {
                            var serverId = res.serverId; // 返回图片的服务器端ID
                            //alert(serverId);

                            $('<input name="file_' + $('#imgs input').length + '" type="hidden">')
                                .val(serverId)
                                .appendTo('#imgs');
                        }
                    });
                    if (window.__wxjs_is_wkwebview) {//判断内核，参考https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1483682025_enmey
                        //WKWebview内核使用新方法获取图片base64，显示即可参考https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421141115，<span style="font-weight:700;color:rgb(34,34,34);font-family:arial, helvetica, sans-serif;font-size:14px;"><span style="font-family:'微软雅黑', 'Microsoft YaHei';">获取本地图片接口</span></span>
                        wx.getLocalImgData({
                            localId: localIds[i], // 图片的localID
                            success: function (res) {
                                var localData = res.localData; // localData是图片的base64数据，可以用img标签显示

                                var div = $('<div class="img-wrap img-thumbnail">')
                                    .on('click', onImgClick);
                                //.prependTo('#imgs');

                                var imgs = $('#imgs')[0];
                                imgs.insertBefore(div, imgs.childNodes[0]);

                                $('<img>')
                                    .prop('src', localData)
                                    .appendTo(div);


                                //$('<input name="file_' + $('#imgs input').length + '" type="hidden">')
                                //    .val(localIds[i])
                                //    .appendTo(div);
                                var btnGroup = $('<div class="btn-group" role="group" aria-label="...">').appendTo(div);
                                $('<button class="btn btn-danger">')
                                    .text('删除')
                                    .on('click', onImgDelete)
                                    .appendTo(btnGroup);
                                //$('<button class="btn btn-danger">')
                                //    .text('关闭')
                                //    .on('click', onImgClick)
                                //    .appendTo(btnGroup);
                            }
                        });
                    }
                    else {
                        var div = $('<div class="img-wrap img-thumbnail">')
                            .on('click', onImgClick)
                            .appendTo('#imgs');
                        var img = $('<img>')
                            .prop('src', res.localIds[i])
                            .appendTo(div);
                        //$('<input name="file_' + $('#imgs input').length + '" type="hidden">')
                        //    .val(res.localIds[i])
                        //    .appendTo(div);


                        $('<button class="btn btn-danger">')
                            .text('删除')
                            .on('click', onImgDelete)
                            .appendTo(div);
                        $('<button class="btn btn-danger">')
                            .text('关闭')
                            .on('click', onImgClose)
                            .appendTo(div);
                    }
                }
            }
        });
    });
});

wx.error(function (res) {
    console.log(res);
    // alert(res);
    // config信息验证失败会执行error函数，如签名过期导致验证失败，
    // 具体错误信息可以打开config的debug模式查看，也可以在返回的res参数中查看，对于SPA可以在这里更新签名。
});

var onImgClick = function () {
    var img = $(this);
    img.toggleClass('preview').off('click', onImgClick);
    return false;
}
var onImgDelete = function () {
    $(this).parents('.img-wrap').remove();
    return false;
}
var onImgClose = function () {

    $(this).parents('.img-wrap').toggleClass('preview');
    $(this).parents('.img-wrap').find('img').on('click', onImgClick);
    return false;
}


$('.resp').userpicker({ params: 'controller=SafetyCheckRecords&action=Receive' });