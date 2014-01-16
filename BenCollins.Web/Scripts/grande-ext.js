(function ($) {
    var $fields = $('article.content.editable header div.placeholder')
    $fields.on('focus', function () {
        if (console) {
            console.log('fukkus!')

            var range = document.createRange();
            range.setStart(this, 0);
            range.setEnd(this, 0);
            var sel = window.getSelection();
            sel.removeAllRanges();
            sel.addRange(range);
        }
    })

    // this is very annoying.  tabindex seems to be broken (or just incomprehensible to me) in Chrome, so...
    $fields.on('keydown keypress keyup', function (e) {
        if (e.which == 9) { // tab
            $fields.eq($(this).index() + 1).focus();
        }
    })

})(jQuery);