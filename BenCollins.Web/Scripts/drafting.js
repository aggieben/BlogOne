(function ($) {
    'use strict';
    var $input = $('textarea#wmd-input')
    var $status = $('.status-text')
    var last = -1;

    $input.on('input', function (e) {
        clearTimeout(last);
        if ($input.val()) {
            last = setTimeout(function () {
                $.post('/post/draft', $('form').serialize(), function success(data, status, jqxhr) {
                    $status.removeClass('failure')
                    $status.addClass('success')
                    $status.text('draft saved at {0:g}'.format(new Date()))
                })
                .fail(function (data) {
                    $status.removeClass('success')
                    $status.addClass('failure')
                    $status.text('draft failed to save: {0}'.format(data))
                })
            }, 500)
        }
    })
}(jQuery))