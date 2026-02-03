function displayProduct(product) {
    console.log("id: ".concat(product.id, ", title: ").concat(product.title, ", price: ").concat(product.price));
}
var apiData = { id: "123", title: "Teclado", price: 450, manufacturer: "TechGear", stock: 15 };
//displayProduct(apiData);

/* ------------------------------------------------------------------------------- */

const hider = document.getElementById("hider");
const textToHide = document.getElementById("text");

function hideText(){
  textToHide.hidden = true;
}

hider.addEventListener("click", hideText);

/* ------------------------------------------------------------------------------- */

const shybuttonElem = document.getElementById("shybutton");

function hideButton(){
    shybuttonElem.hidden = true;
}

shybuttonElem.addEventListener("click", hideButton);

/* ------------------------------------------------------------------------------- */

const ball = document.getElementById("ball");
const field = document.getElementById("field")
let fieldCoords = field.getBoundingClientRect()

function moveBall(event){

    //console.log(fieldCoords);

    ballposition = {
        left : event.clientX - fieldCoords.left - field.clientLeft - ball.offsetWidth/2,
        top : event.clientY - fieldCoords.top - field.clientTop - ball.offsetHeight/2
    }

    if (ballposition.left < 0) ballposition.left = 0;
    if (ballposition.top < 0) ballposition.top = 0;

    if (ballposition.left > field.clientWidth - ball.offsetWidth){
        ballposition.left = field.clientWidth - ball.offsetWidth;
    }

    if (ballposition.top > field.clientHeight - ball.offsetHeight){
        ballposition.top = field.clientHeight - ball.offsetHeight;
    }

    ball.style.left = ballposition.left + 'px';
    ball.style.top = ballposition.top + 'px';


}

field.addEventListener("click", moveBall)

/* ------------------------------------------------------------------------------- */

const TOGGLE_ON = "▼"
const TOGGLE_OFF = "▶"

let toggler = document.getElementById("toggler");
let toggleList = document.getElementById("toggle-list")
let arrowToggler = document.getElementById("arrow-toggler")

function showToggleables(){

    let opened = swapSymbol();
    //let listItems = document.querySelectorAll('#toggleList li')
    toggleList.hidden = !opened;

}

function swapSymbol(){
    let arrowState = arrowToggler.textContent;
    if (arrowState.includes(TOGGLE_OFF)){
        arrowToggler.textContent = arrowState.replace(TOGGLE_OFF, TOGGLE_ON);
        return true;
    } else {
        arrowToggler.textContent = arrowState.replace(TOGGLE_ON, TOGGLE_OFF);
        return false;
    }
}

toggler.addEventListener("click", showToggleables);

/* ------------------------------------------------------------------------------- */

let removeButtons = document.querySelectorAll("button.remove-button")
removeButtons.forEach((button) => button.addEventListener('click', deleteBlock))

function deleteBlock(clickingEvent){
    let button = clickingEvent.currentTarget
    //console.log(button);
    button.parentElement.remove()
}

/* ------------------------------------------------------------------------------- */


const carousel = document.getElementById('emoji-carousel');
const list = carousel.querySelector('ul');
const listElems = carousel.querySelectorAll('li');

let width = 130;
let count = 3;
let position = 0;

document.getElementById('arrow-previous').onclick = function() {
  position += width * count;
  position = Math.min(position, 0);
  list.style.marginLeft = position + 'px';
};

document.getElementById('arrow-next').onclick = function() {
  position -= width * count;
  let maxScroll = -width * (listElems.length - count);
  position = Math.max(position, maxScroll);
  list.style.marginLeft = position + 'px';
};