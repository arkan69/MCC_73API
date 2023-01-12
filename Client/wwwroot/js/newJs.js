////CHART
//$.ajax({
//    url:"https://localhost:7234/api/Employees"
//}).done((data) => {
//    console.log(data);
//    var gender = data.data
//        .map(x => ({ gender: x.gender }));
//    var { gender0, gender1 } = gender.reduce((previous, current) => {
//        if (current.gender === 0) {
//            return { ...previous, gender0: previous.gender0 + 1 }
//        } if (current.gender === 1) {
//            return { ...previous, gender1: previous.gender1 + 1 }
//        }
//    }, { gender0: 0, gender1: 0 })

//    //PIE CHART
//    var options = {
//        chart: {
//            type: 'pie',
//            height: 350
//        },
//        series: [gender0, gender1],
//        labels: ['Male', 'Female']
//    }
//    var chart = new ApexCharts(document.querySelector("#chart"), options);
//    chart.render();

//    //BAR CHART
//    var chartku = {
//        series: [{
//            data: [gender0, gender1]
//        }],
//        chart: {
//            type: 'bar',
//            height: 350
//        },
//        plotOptions: {
//            bar: {
//                borderRadius: 2,
//                horizontal: false,
//            }
//        },
//        dataLabels: {
//            enabled: false
//        },
//        xaxis: {
//            categories: ['Male', 'Female'],
//        },

//    }
//    var chart = new ApexCharts(document.querySelector("#chart1"), chartku);
//    chart.render();
//});

let male = 0
let female = 0
$.ajax({
    url: "https://localhost:7234/api/Employees",
    dataType: "json",
    success: function (result) {
        for (var i = 0; i < result.data.length; i++) {
            if (result.data[i].gender == 0) {
                male += 1;
            } else {
                female += 1;
            }
        }

        //PIE CHART
        var options = {
            chart: {
                type: 'pie',
                height: 250
            },
            series: [male, female],
            labels: ['Male', 'Female']
        }
        var chart = new ApexCharts(document.querySelector("#chart"), options);
        chart.render();

        //BAR CHART
    var chartku = {
        series: [{
            data: [male, female]
        }],
        chart: {
            type: 'bar',
            height: 250
        },
        plotOptions: {
            bar: {
                borderRadius: 2,
                horizontal: false,
            }
        },
        dataLabels: {
            enabled: false
        },
        xaxis: {
            categories: ['Male', 'Female'],
        },

    }
    var chart = new ApexCharts(document.querySelector("#chart1"), chartku);
    chart.render();
    }
});

//TABLE
let table = $('#temployee').DataTable({
    dom: '<"top"Blf>rtip',
    buttons: [
        {
            extend: 'copyHtml5',
            className: 'btn btn-secondary',
            exportOptions: {
                columns: ':visible'
            },
            attr: {
                title: 'Copy data',
                'data-bs-toggle': 'tooltip',
                'data-bs-placement': 'top'
            }
        },
        {
            extend: 'excelHtml5',
            className: 'btn btn-success',
            exportOptions: {
                columns: ':visible'
            },
            attr: {
                title: 'Export to Excel',
                'data-bs-toggle': 'tooltip',
                'data-bs-placement': 'top'
            }
        },
        {
            extend: 'csvHtml5',
            className: 'btn btn-info',
            exportOptions: {
                columns: ':visible'
            },
            attr: {
                title: 'Export to CSV',
                'data-bs-toggle': 'tooltip',
                'data-bs-placement': 'top'
            }
        },
        {
            extend: 'pdfHtml5',
            className: 'btn btn-danger',
            exportOptions: {
                columns: ':visible'
            },
            attr: {
                title: 'Export to PDF',
                'data-bs-toggle': 'tooltip',
                'data-bs-placement': 'top'
            }
        },
        {
            extend: 'colvis',
            className: 'btn btn-dark',
            attr: {
                title: 'Ignore Column',
                'data-bs-toggle': 'tooltip',
                'data-bs-placement': 'top'
            }
        }

    ],
    ajax: {
        url: "https://localhost:7234/api/Employees",
        dataType: "Json",
        dataSrc: "data" //need notice, kalau misal API kalian 
    },
    columns: [
        {
            "data": "nik"
        },
        {
            "data": "firstName"
        },
        {
            "data": "lastName"
        },
        {
            "data": "phone"
        },
        {
            "data": "birthDate"
        },
        {
            "data": "salary",
            render: function (data, type, row) {
                return `Rp. ` + data;
            }
        },
        {
            "data": "email"
        },
        {
            "data": "gender",
            render: function (data, type, row) {
                if (row.gender == 0) {
                    return "Male";
                }
                else {
                    return "Female";
                }
            }
        },
        {
            "data": "nik",
            render: function (data) {
                return `<button class="btn btn-warning" onclick="Edit(\'${data}'\)" data-bs-toggle="modal" data-bs-target="#modalInsert">Edit</button>
                    <button class="btn btn-danger" onclick="Delete(\'${data}'\)" data-bs-toggle="tooltip" data-original-title="Edit user">Delete</button>`;
            }
        }
    ]
});


//FORM VALIDATE
$(document).ready(function () {
    $("#formInsertEmployee").validate({
        /*code to make a sign on input element, if they're empty 
        and the user click submit button, the input field will turn into red*/
        error: function (label) {
            $(this).addclass("error");
        },
        rules: {
            nik: {
                required: true,
                minlength: 5,
                maxlength: 5
            },
            firstName: {
                required: true
            },
            lastName: {
                required: true
            },
            phone: {
                required: true
            },
            date: {
                required: true
            },
            salary: {
                required: true
            },
            email: {
                required: true,
                email: true
            },
            gender: {
                required: true
            }
        },
        messages: {
            nik: {
                required: "NIK is Required.",
                minlength: "Min length is 5",
                maxlength: "Max length is 5"
            },
            firstName: {
                required: "First Name is Required."
            },
            lastName: {
                required: "Last Name is Required."
            },
            phone: {
                required: "Phone Number is Required."
            },
            date: {
                required: "Birth Date is Required."
            },
            salary: {
                required: "Salary is Required."
            },
            email: {
                required: "Email is Required.",
                email: "Form must be email!"
            },
            gender: {
                required: "Gender is Required."
            }
        },
        submitHandler: () => {
            if (check) { //CONDITION FOR MODAL, TO INSERT FUNCTION OR UPDATE FUNCTION
                Update();
            }
            else {
                Insert()
            }
        }
    });
});

//INSERT
const Insert = () => {
    let employee = {
        //NIK ETC are sensitive case, so watch your column name on your database
        nik: $("#nik").val(),
        firstName: $("#firstName").val(),
        lastName: $("#lastName").val(),
        phone: $("#phone").val(),
        birthDate: $("#date").val(),
        salary: parseInt($("#salary").val()),
        email: $("#email").val(),
        gender: parseInt($("#gender").val())
    }

    console.log(employee);

    $.ajax({
        url: "https://localhost:7234/api/Employees",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(employee),
    }).done((result) => {
        console.log("success");
        $("#modalInsert").modal("hide");
        Swal.fire({
            text: 'New Employee Created!',
            icon: 'success',
            timer: 5000,
            timerProgressBar: true
        });
        table.ajax.reload()
    }).fail((error) => {
        console.log("failed");
        $("#modalInsert").modal("hide");
        Swal.fire({
            text: 'Error Creating Project!',
            icon: 'error',
            timer: 5000,
            timerProgressBar: true
        });
    })
};

//variable check to help the modal will be pass to insert function or update function 
let check

//this to pass the modal on html
const Add = () => {
    check = false
    $("#nik").attr("disabled", false);
}

//GET EMPLOYEE BY ID TO FILL THE MODAL FORM WITH EMPLOYEE VALUE THAT WANT TO BE EDITED
const Edit = (key) => {
    check = true
    $.ajax({
        url: `https://localhost:7234/api/Employees/${key}`
    }).done((result) => {
        $("#nik").val(result.data.nik),
            $("#nik").attr("disabled", true);
        $("#firstName").val(result.data.firstName),
            $("#lastName").val(result.data.lastName),
            $("#phone").val(result.data.phone),
            $("#date").val(result.data.birthDate),
            $("#salary").val(result.data.salary),
            $("#email").val(result.data.email),
            $("#gender").val(result.data.gender)
    })
}

//Function to update data(put methode)
const Update = () => {
    let updateEmployee = {
        nik: $("#nik").val(),
        firstName: $("#firstName").val(),
        lastName: $("#lastName").val(),
        phone: $("#phone").val(),
        birthDate: $("#date").val(),
        salary: parseInt($("#salary").val()),
        email: $("#email").val(),
        gender: parseInt($("#gender").val())
    }
    console.log(updateEmployee);

    $.ajax({
        url: `https://localhost:7234/api/Employees`,
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify(updateEmployee),
    }).done((result) => {
        console.log("Udpdate Data success");
        $("#modalInsert").modal("hide");
        Swal.fire({
            text: `Employee has been Updated`,
            icon: 'success',
            timer: 5000,
            timerProgressBar: true
        });
        table.ajax.reload()
    }).fail((error) => {
        console.log("Udpdate Data Failed");
        $("#modalInsert").modal("hide");
        Swal.fire({
            text: `Failed to Update Employee`,
            icon: 'error',
            timer: 5000,
            timerProgressBar: true
        });
    })
}

//Delete function to delete the entire selected data
const Delete = (key) => {
    Swal.fire({
        title: 'Are you sure?',
        text: 'You wont able to revert this!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'DELETE',
                url: `https://localhost:7234/api/Employees/${key}`,
                success: () => {
                    Swal.fire(
                        'Deleted!',
                        'Employee has been deleted.',
                        'success'
                    )
                    table.ajax.reload()
                },
                error: () => {
                    Swal.fire(
                        'Failed!',
                        'Error deleting employee.',
                        'error'
                    )
                }
            })
        }
    })
}

