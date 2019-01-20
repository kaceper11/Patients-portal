
$(document).ready(function () {

    $('#subject #description').on('keyup', function () {
        if ($('#subject').valid()
            || $('#description').valid()) {
            $('#zglos').prop("disabled", false);
        } else {
            $('#zglos').prop("disabled", true);
        }
    });
});