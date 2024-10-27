// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("input[name='AppointmentDate']").change(function () {
        var appointmentDate = $(this).val();
        $.getJSON('/Appointments/GetBusyTimes', { date: appointmentDate }, function (data) {
            $("#StartTime").empty();
            for (var i = 14; i <= 17; i++) {
                var time = i + ":00";
                if (!data.includes(time)) {
                    $("#StartTime").append('<option value="' + time + '">' + time + '</option>');
                }
            }
        });
    });
});
$(document).ready(function () {
    // Belirli bir işlemi başlatma
    $('.delete-button').on('click', function (e) {
        if (!confirm("Bu randevuyu silmek istediğinize emin misiniz?")) {
            e.preventDefault(); // Silme işlemini iptal et
        }
    });
});