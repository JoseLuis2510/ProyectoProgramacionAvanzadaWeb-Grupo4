$(document).ready(function () {
    $("#Actualizarform").submit(function (e) {
        e.preventDefault();

        Swal.fire({
            title: '¿Deseas guardar los cambios?',
            showCancelButton: true,
            confirmButtonText: 'Sí, guardar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                var form = $(this);

                $.ajax({
                    type: form.attr('method') || 'POST',
                    url: form.attr('action'),
                    data: form.serialize(),
                    success: function (response) {
                        // Aquí suponemos que el servidor responde con un resultado exitoso
                        Swal.fire({
                            icon: 'success',
                            title: '¡Datos actualizados!',
                            text: 'La información fue guardada correctamente.',
                            timer: 2000,
                            showConfirmButton: false
                        }).then(() => {
                            window.location.href = "/Home/Index";
                        });
                    },
                    error: function () {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'Ocurrió un problema al guardar los datos.'
                        });
                    }
                });
            }
        });
    });
});


$(document).ready(function () {
    $("#Citaform").submit(function (e) {
        e.preventDefault();

        Swal.fire({
            title: '¿Deseas agendar la cita?',
            showCancelButton: true,
            confirmButtonText: 'Sí, agendar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                var form = $(this);

                $.ajax({
                    type: form.attr('method') || 'POST',
                    url: form.attr('action'),
                    data: form.serialize(),
                    success: function (response) {
                        Swal.fire({
                            icon: 'success',
                            title: '¡Cita Agendada!',
                            text: 'La información fue guardada correctamente.',
                            timer: 2000,
                            showConfirmButton: false
                        }).then(() => {
                            window.location.href = "/Cita/MisCitas";
                        });

                    },
                    error: function () {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'Ocurrió un problema al agendar la cita.'
                        });
                    }
                });
            }
        });
    });
});

$(document).ready(function () {
    $("#EliminarCitaform").submit(function (e) {
        e.preventDefault();

        Swal.fire({
            title: '¿Deseas eliminar la cita?',
            showCancelButton: true,
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                var form = $(this);

                $.ajax({
                    type: form.attr('method') || 'POST',
                    url: form.attr('action'),
                    data: form.serialize(),
                    success: function (response) {
                        Swal.fire({
                            icon: 'success',
                            title: '¡Cita Eliminada!',
                            text: 'La información fue eliminada correctamente.',
                            timer: 2000,
                            showConfirmButton: false
                        }).then(() => {
                            window.location.href = "/Home/Index";
                        });
                    },
                    error: function () {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'Ocurrió un problema al eliminar la cita.'
                        });
                    }
                });
            }
        });
    });
});

$(document).ready(function () {
    $("#CrearHorarioform").submit(function (e) {
        e.preventDefault();

        Swal.fire({
            title: '¿Deseas crear el horario?',
            showCancelButton: true,
            confirmButtonText: 'Sí, crear',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                var form = $(this);

                $.ajax({
                    type: form.attr('method') || 'POST',
                    url: form.attr('action'),
                    data: form.serialize(),
                    success: function (response) {
                        Swal.fire({
                            icon: 'success',
                            title: '¡Horario Creado!',
                            text: 'La información fue guardada correctamente.',
                            timer: 2000,
                            showConfirmButton: false
                        }).then(() => {
                            window.location.href = "/Horario/VerHorario";
                        });

                    },
                    error: function () {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'Ocurrió un problema al crear el horario.'
                        });
                    }
                });
            }
        });
    });
});

$(document).ready(function () {
    $("#EliminarHorarioform").submit(function (e) {
        e.preventDefault();

        Swal.fire({
            title: '¿Deseas eliminar el horario?',
            showCancelButton: true,
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                var form = $(this);

                $.ajax({
                    type: form.attr('method') || 'POST',
                    url: form.attr('action'),
                    data: form.serialize(),
                    success: function (response) {
                        Swal.fire({
                            icon: 'success',
                            title: '¡Horario Eliminado!',
                            text: 'La información fue eliminada correctamente.',
                            timer: 2000,
                            showConfirmButton: false
                        }).then(() => {
                            window.location.href = "/Horario/VerHorario";
                        });
                    },
                    error: function () {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'Ocurrió un problema al eliminar el horario.'
                        });
                    }
                });
            }
        });
    });
});


$(document).ready(function () {
    $("#AtenderCitaform").submit(function (e) {
        e.preventDefault();

        Swal.fire({
            title: '¿Deseas atender al paciente?',
            showCancelButton: true,
            confirmButtonText: 'Sí, atender',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                var form = $(this);

                $.ajax({
                    type: form.attr('method') || 'POST',
                    url: form.attr('action'),
                    data: form.serialize(),
                    success: function (response) {
                        Swal.fire({
                            icon: 'success',
                            title: '¡Paciente Atendido!',
                            text: 'La información fue procesada correctamente.',
                            timer: 2000,
                            showConfirmButton: false
                        }).then(() => {
                            window.location.href = "/Cita/ObtenerCitasTotales";
                        });
                    },
                    error: function () {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'Ocurrió un problema al procesar la información.'
                        });
                    }
                });
            }
        });
    });
});