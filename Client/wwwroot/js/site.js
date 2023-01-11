// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//// Write your JavaScript code.
//let judul = document.getElementById("judul");
//judul.innerHTML = 'JUDULKU';

//let paragraf = document.getElementsByTagName("p")[0];
////paragraf.style.backgroundColor = "red";

//let div = document.getElementById("div1");
//div.style.backgroundColor = "skyblue";

//let var1 = document.getElementBy

//JAVASCRIPT
//Array
//let array = [1, 2, 3, 4];
////insert last
//array.push("halo");
//console.log(array);
////delete last
//array.pop();
//console.log(array);
////insert first
//array.unshift("test");
//console.log(array);
////delete first
//array.shift()
//console.log(array);

////array multi dimensi
//let arraymulti = [1, 2, 3, ['a', 'b', 'c'], true];
//console.log(arraymulti);

//let tambah = (x, y) => { return x + y; }
//console.log(tambah(5, 10));

////object
//const mhs = {
//    nama: "Budi",
//    nim: "a11201510469",
//    gender: "laki",
//    hobby: ["mancing", "tawuran", "ngegame"],
//    isActive: true
//};
//console.log(mhs);

//let user = {};
//user.username = "budilagi";
//user.pass = "passwordbudi";
//console.log(user);

////array of object
//let animals = [
//    {
//        nama: "budi", species: "dog", class: { name: "mamalia" },
//        nama: "krucil", species: "dog", class: { name: "mamalia" },
//        nama: "nemo", species: "fish", class: { name: "pisces" },
//        nama: "yuki", species: "fish", class: { name: "pisces" },
//        nama: "kori", species: "dog", class: { name: "mamalia" },
//    }
//]

//console.log(animals[0].class.name);

//const onlyDog = [];
//for (let i; i = animals.length; i++) {
//    if (animals.species == "dog") {
//        dog.push(animals[i]);
//    }
//}

//onlyDog.push(animals.filter(animal => animal.species == "dogs"));
//console.log(dogs);

//const detailAnimal;
//detailAnimal = animals.map(animal => {
//    return {
//        name: animal.name,
//        species: animal.species,
//        isFish: animal.species == "fish" ? true : false
//    }
//})
//console.log(detailAnimal);

//JQUERY
//$("#judull").click(function () {
//    $("#judul").css("background-color", "cyan");
//    $("#judul").html("Berubah!!!!");
//})

//AJAX
$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/",
    //success: function (result) {
    //    console.log(result);
    //}
}).done((result) => {
    //console.log(result.results);
    let temp = ""
    //for (var i = 0; i < result.results.length; i++) {
    //    temp += "<li>" + result.results[i].name +"</li>";
    //}
    //$("#listPoke").html(temp);

    $.each(result.results, function (key, val) {
        //old way
        //temp += "<tr>\
        //            <td>"+(key+1)+"</td>\
        //            <td>"+val.name+"</td>\
        //            <td>"+val.name+"</td>\
        //        </tr>"
        //jalan ninja ku / pakai backtick / literal template
        temp += `<tr>
                    <td>${key + 1}</td>
                    <td>${val.name}</td>
                    <td><button class="btn btn-primary" onclick="detailPoke('${val.url}')" data-bs-toggle="modal" data-bs-target="#modalPokeDetail">Detail</button></td>
                </tr>`
    })
    $("#tablePoke").html(temp);

}).fail((err) => {
    console.log(err);
})

function detailPoke(stringUrl) {
    $.ajax({
        url: stringUrl
    }).done((result) => {
        let imgSrc = result.sprites.other.dream_world.front_default;
        let name = result.name;
        let color = "";
        let flavorText = "";
        let habitat = "";
        let weight = result.weight / 10;
        let height = result.height / 10;
        let speciesUrl = result.species.url
        let types = result.types;
        let tipe = "";
        let modalTitleColor = "white";
        let indexType = 0;
        types.forEach(type => {
            let typeColor = ""
            if (indexType % 2 == 0) {
                typeColor = "primary"
            }
            if (indexType % 2 != 0) {
                typeColor = "secondary"
            }
            tipe += `<span class="badge-color-black rounded bg-${typeColor}">${type.type.name}</span>` + "\n"
            indexType = indexType + 1;
        });
        let abilities = result.abilities
        let able = "";
        let indexAbility = 0;
        abilities.forEach(ability => {
            able += `<span class="badge-color-black rounded bg-info">${ability.ability.name}</span>` + "\n"
        });

        let stats = result.stats
        let status = ""
        stats.forEach(stat => {
            let statUp = upperCase(stat.stat.name)
            let barColor = ""
            if (stat.base_stat >= 70) {
                barColor = "success"
            }
            if (stat.base_stat >= 40 && stat.base_stat < 70) {
                barColor = "warning"
            }
            if (stat.base_stat < 40) {
                barColor = "danger"
            }
            status += `<tr>
                        <td>${statUp}</td>
                        <td>
                            <div class="progress">
                                <div class="progress-bar bg-${barColor}" role="progressbar" aria-label="Success example" style="width: ${stat.base_stat}%" aria-valuenow="${stat.base_stat}" aria-valuemin="0" aria-valuemax="100">${stat.base_stat}%</div>
                            </div>
                        </td>
                        </tr>`+ "\n"
        });

        $.ajax({
            url: speciesUrl
        }).done((species) => {
            color = species.color.name
            if (color == "white") {
                modalTitleColor = "black"
            }
            habitat = species.habitat.name
            flavorTexts = species.flavor_text_entries
            let lengthFlavor = flavorTexts.length
            let i = 0
            while (i < lengthFlavor) {
                if (flavorTexts[i].language.name == "en") {
                    console.log("language = " + flavorTexts[i].language.name)
                    flavorText = flavorTexts[i].flavor_text
                    break
                }
                i++
            }
            flavorTextString = flavorText.replace("\u000c", "\n")

            $(".modal-header").css('background', color)
            $(".modal-title").css('color', modalTitleColor)
            $(".flavor-text").html(`<blockquote>${flavorTextString}</blockquote>`)
            $("#habitatPoke").html(sentenceCase(habitat))
        }).fail((error) => {
            console.log(error);
        })
        $("#imagePoke").attr("src", imgSrc)
        $("#namePoke").html(sentenceCase(name))
        $("#pokeName").html(sentenceCase(name))
        $("#pokeWeight").html(weight + " kg")
        $("#pokeHeight").html(height + " m")
        $("#pokeType").html(tipe)
        $("#pokeAbility").html(able)
        $("#pokeStatus").html(status)
    }).fail((error) => {
        console.log(error);
    })
}

function sentenceCase(str) {
    let sentenceCase = str.charAt(0).toUpperCase() + str.slice(1);
    return sentenceCase;
}

function upperCase(str) {
    let upperCase = str.toUpperCase();
    return upperCase;
}

$(document).ready(function () {
    $('#example').DataTable({
        ajax: {
            url: "https://pokeapi.co/api/v2/pokemon/",
            dataType: "Json",
            dataSrc: "results" //need notice, kalau misal API kalian 
        },
        columns: [
            {
                "data": "name"
            },
            {
                "data": "name"
            },
            {
                "data": "url",
                render: function (data, type, row) {
                    return `<button class="btn btn-primary" onclick="detailPoke('${data}')" data-bs-toggle="modal" data-bs-target="#modalPokeDetail">Detail</button>`;
                }
            }
        ]
    });
});