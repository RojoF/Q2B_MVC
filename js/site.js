
// Activa el modal al iniciar pagina si no hay cookie
$(document).ready(function () {

    //Obtenemos cookie
    var value = Cookies.get('permiso')

    //Configuramos toastr al iniciar pagina
    toastr.options = {
        "positionClass": "toast-bottom-right",
        "progressBar": true,
        "showDuration": "1000"
    }

    if (value == null) {
        // id de nuestro modal
        $('#myModal').modal('show')
        getFocus()

    }
})


//Metodo que comprueba que el formato de la fecha sea correcta y una fecha valida
function validarFecha() {

    var fecha = document.getElementById('edad').value

    // Primero verifica el patron
    var isValid = fecha.match(/^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$/)
    if (!isValid) error = true

    // Mediante el delimitador "/" separa dia, mes y año
    var fecha = fecha.split('/')
    var day = parseInt(fecha[0])
    var month = parseInt(fecha[1])
    var year = parseInt(fecha[2])

    // Verifica que dia, mes, año, solo sean numeros
    error = isNaN(day) || isNaN(month) || isNaN(year)

    // Lista de dias en los meses, por defecto no es año bisiesto
    var ListofDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31]
    if (month === 1 || month > 2)
        if (
            day > ListofDays[month - 1] ||
            day < 0 ||
            ListofDays[month - 1] === undefined
        )
            error = true

    // Detecta si es año bisiesto y asigna a febrero 29 dias
    if (month === 2) {
        var lyear = (!(year % 4) && year % 100) || !(year % 400)
        if (lyear === false && day >= 29) error = true
        if (lyear === true && day > 29) error = true
    }

    if (error === true) {
        toastr.warning(
            "Por favor, utiliza el formato DD/MM/YYYY Ej-12/05/1984",
            "Formato de fecha Incorrecto");
        borrar()
        getFocus()

        return false

    } else calcularEdad()
}


//Metodo para cambiar formato de fecha establecido y calcular la edad a.p de la fecha
function calcularEdad() {
    var fecha = document.getElementById('edad').value
    var d = new Date(fecha.split('/').reverse().join('-'))
    var dd = d.getDate()
    var mm = d.getMonth() + 1
    var yy = d.getFullYear()
    Nfecha = yy + '/' + mm + '/' + dd
    var cumple = new Date(Nfecha)
    var hoy = new Date()
    var edad = hoy.getFullYear() - cumple.getFullYear()
    var m = hoy.getMonth() - cumple.getMonth()

    if (m < 0 || (m === 0 && hoy.getDate() < cumple.getDate())) {
        edad--
    }
    return validaEdad(edad)
}

//Funcion para validar edad y establecer el cookie
function validaEdad(edad) {
    //Obtener valor de edad del input del popup
    var v = edad
    var edadMax = 18

    //Si es mayor se guarda en la cookie y continua la navegacion de la web
    if (edad >= edadMax) {

        var expiration = new Date()
        var minutes = 5
        expiration.setTime(expiration.getTime() + minutes * 60 * 1000)
        Cookies.set('permiso', edad, { expires: expiration })
        toastr.success("Ya puedes navegar ", "Muchas gracias");
        $('#myModal').modal('hide')
        //location.reload();

    } else {
        toastr.error("Vuelve cuando seas mayor de edad, Adios!!",
            "Lo sentimos, no podemos enseñarte el contenido");

        labelHide()
    }
}

//Metodo para si es menor cambiar modal
function labelHide() {
    $('#edad').hide()
    $('#label').hide()
    $('#close').hide()
    document.getElementById("salir").style.display = "block";
    document.getElementById("label_dos").style.display = "block";
    document.getElementById("bye").style.display = "block";

}

// Metodo para hacer focus a input
function getFocus() {
    document.getElementById("edad").focus();
}

// Metodo para borrar input 
function borrar() {
    $(document).ready(function () {
        $('#edad').val('')
    })
}

//Funcion para solo escribir numeros en el campo input del modal
function soloNumeros(evt) {
    $('#edad').on('input', function () {
        this.value = this.value.replace(/[^0-9/]/g, '')
    })
}
