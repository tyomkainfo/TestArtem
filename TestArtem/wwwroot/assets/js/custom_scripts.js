
$(document).ready(function () {
    $('#mainCheckbox').click(function () {
        $('.childCheckbox').prop('checked', this.checked);
    });
    $(".owl-carousel").owlCarousel({
        loop: true,
        margin: 0,
        nav: true,
        autoplay: true,
        autoplayTimeout: 2000,
        responsive: {
            0: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    });
});
