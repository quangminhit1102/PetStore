var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {

    }

    $('.bx bx-trash').off('click').on('click', function (e) {
        e.preventDefault();
        $.ajax({
            data: { id: $(this).data('id') },
            url: '/Cart/Delete',
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                if (res.status == true) {
                    window.location.href = "/gio-hang";
                }
            }
        })
    });
}
cart.init();