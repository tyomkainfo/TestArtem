
$(document).ready(function () {
    $('#mainCheckbox').click(function () {
        $('.childCheckbox').prop('checked', this.checked);
    });
});
