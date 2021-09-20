

const p = document.getElementsByTagName('p');

/*p.forEach((data) => 
    console.log(data)
)*/
/*for (const data of p) {
    *//*console.log(data)*//*
    *//*data.style.backgroundColor = 'red';*//*
    data.addEventListener('click', function () {
        alert("asd")
    })
}*/

/*function gantiWarna(color){
    this.
}*/

const animals = [
    {
        name: "Cat1", species: "cat", class: {name: "mamalia"}
    },
    {
        name: "Dog1", species: "dog", class: { name: "vertebrata" }
    },
    {
        name: "Dog2", species: "dog", class: { name: "vertebrata" }
    },
    {
        name: "Cat2", species: "cat", class: { name: "mamalia" }
    },
    {
        name: "Cat3", species: "cat", class: { name: "mamalia" }
    },
    {
        name: "Cat4", species: "cat", class: { name: "mamalia" }
    },
]

function convertDogToNonMamal() {
    for (let animal of animals) {
        if (animal.species == "dog") {
            animal.class.name = 'non-mamalia'
            /*console.log(animal.class.name)*/
        }
    }
}

let result = []

function onlyCat() {
    result = animals.filter(animal => animal.species == "cat")
}