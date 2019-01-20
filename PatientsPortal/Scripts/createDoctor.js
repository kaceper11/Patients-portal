$("#user-roles").change(function () {
    if ($("#user-roles").val() == 3) {

        if ($(".doctor-attributes").hasClass('k-visible')) {

        } else {
            $(".doctor-attributes").addClass('k-visible');
            $(".doctor-attributes").removeClass('k-invisible');
        }

    } else {
        if ($(".doctor-attributes").hasClass('k-invisible')) {

        } else {
            $(".doctor-attributes").addClass('k-invisible');
            $(".doctor-attributes").removeClass('k-visible');
        }
    }
});