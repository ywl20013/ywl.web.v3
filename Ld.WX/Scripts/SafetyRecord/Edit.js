ywl.ajax({
    processUrl: false,
    url: 'http://www.bjdflld.com/services/SafetyCheckRecord/' + Id,
    //data: "name=John&location=Boston",
    success: function (ret) {
        var data = ret;
        for (var prop in data) {

            var el = $('#' + prop);
            if (el.length > 0) {
                if (el.is('input') || el.is('select')) {
                    el.val(data[prop]);
                    if (prop == 'ResponsiblePerson') {
                        var input_display = el.siblings('input.display');
                        if (input_display.length == 0) {
                            input_display = $('<input class="form-control display">').prependTo(el.parent());
                        }
                        input_display.val(data['ResponsiblePersonName']);
                        el.hide();
                    }
                    else if (prop == 'DangerType') {
                        var chk = el.parents('.btn-checkbox');
                        var btnGroup = $('.btn-group', chk);
                        $('.btn', btnGroup).removeClass('btn-primary').removeClass('btn-default').addClass('btn-default');
                        $('.btn[data-value="' + data['DangerType'] + '"]', btnGroup).removeClass('btn-default').addClass('btn-primary');
                    }
                }

            }
            if (prop == "Attachments") {
                for (var attach in data[prop]) {
                    var div = $('<div class="img-wrap img-thumbnail">')
                        .on('click', onImgClick)
                        .appendTo('#imgs');
                    var url = data[prop][attach].Url;
                    var img = $('<img>')
                        .prop('src', window.webServices + url.replace('~', ''))
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
    }
});