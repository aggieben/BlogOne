(function () {
    String.prototype.format = function () {
        return String.format.apply(undefined, [this].concat(Array.prototype.slice.call(arguments)));
    }
}())
