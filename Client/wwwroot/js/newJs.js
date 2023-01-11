var options = {
    chart: {
        type: 'pie'
    },
    series: [35, 45, 13, 33],
    labels: ['Apple', 'Mango', 'Orange', 'Watermelon']
}

var chart = new ApexCharts(document.querySelector("#chart"), options);

chart.render();

var chartku = {
    series: [],
    chart: {
        type: 'donut'
    },
    dataLabels: {
        enabled: true
    },
    title: {
        text: 'Employee Gender',
    },
    noData: {
        text: 'Loading...'
    }
};

var chart = new ApexCharts(document.querySelector("#chart1"), chartku);
chart.render();


$.getJSON('https://localhost:7234/api/Employees', function (response) {
    chart.updateSeries([{
        name: 'gender',
        data: response
    }])
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
            }
        },
        {
            extend: 'excelHtml5',
            className: 'btn btn-success',
            exportOptions: {
                columns: ':visible'
            }
        },
        {
            extend: 'csvHtml5',
            className: 'btn btn-info',
            exportOptions: {
                columns: ':visible'
            }
        },
        {
            extend: 'pdfHtml5',
            className: 'btn btn-danger',
            exportOptions: {
                columns: ':visible'
                //columns: [0, 1, 2, 3, 5, 6, 7]
            }
        },
        {
            extend: 'colvis',
            className: 'btn btn-dark'
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

$(document).ready(function () {
    $("#formInsertEmployee").validate({
        //error: function (label) {
        //    $(this).addClass("error");
        //},
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
            if (check) {
                Update();
            }
            else {
                Insert()
            }
        }
    });
});

const Insert = () => {
    let employee = {
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

let check

const Add = () => {
    check = false
    $("#nik").attr("disabled", false);
}
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

