$.ajax({
    url: 'https://pokeapi.co/api/v2/pokemon/'
}).done(res => {
    let htmlContent = "";
    console.log(res.results)

    $.each(res.results, (key, value) => {
        htmlContent += `<tr>
                            <td>${key + 1}</td>
                            <td>${value.name}</td>
                            <td>${value.url}</td>
                            <td>
                                <button
                                    type="button"
                                    class="item-detail btn btn-primary"
                                    data-toggle="modal"
                                    data-target="#pokemonDetailModal"
                                    onClick="pokemonDetail('${value.url}')">Detail</button>
                            </td>
                        </tr>`
    });

    $('.data-pokemon').html(htmlContent)
})

const pokemonDetail = (url) => {
    $.ajax({
        url: url
    }).done(res => {

        let abilities = "";
        let types = ""

        res.abilities.map(item => {
            abilities += `<li>${item.ability.name}</li>`
        })



        let pokeDetailContent = `
                                <img
                                    src="${res.sprites.other.dream_world.front_default}"
                                    class="mx-auto d-block mb-3"
                                    alt="pokeimg"
                                    width="350px"
                                    height="350px" />
                                    <div class="text-center">${types}</div>
                                <ul>
                                    <li>Name: ${res.name} </li>
                                    <li>Weight: ${res.height} </li>
                                    <li>Height: ${res.weight}</li>
                                    <li>Abilities:
                                        <ul>
                                            ${abilities}
                                        </ul>
                                    </li>
                                </ul>`;

        $('#pokemonDetailModal .modal-body').html(pokeDetailContent);
        $('h5.modal-title').html(`${res.name}`);
    });
}

$.ajax({
    url: 'https://localhost:44300/API/Persons/GetPerson/'
}).done(res => {
    for (let r of res.result) {
        console.log(r.fullName)
    }
    /*console.log(res.result)*/
})