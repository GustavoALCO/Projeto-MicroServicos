import { Component, HostListener } from '@angular/core';
import { CardHouseComponent } from '../card-house/card-house.component';
import { CardComponent } from '../card/card.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-carrosel-ads',
  standalone: true,
  imports: [CardComponent, CommonModule],
  templateUrl: './carrosel-ads.component.html',
  styleUrl: './carrosel-ads.component.scss'
})
export class CarroselAdsComponent {
    screenWidth!:number

 items = [
    { titulo: 'Card 1', preco: 123 },
    { titulo: 'Card 2', preco: 213 },
    { titulo: 'Card 3', preco: 231 },
    { titulo: 'Card 4', preco: 313 },
    { titulo: 'Card 5', preco: 231 },
    { titulo: 'Card 6', preco: 231 },
    { titulo: 'Card 7', preco: 231 },
    { titulo: 'Card 8', preco: 231 },
    { titulo: 'Card 9', preco: 231 },
    { titulo: 'Card 10', preco: 231 },
    { titulo: 'Card 11', preco: 231 },
    { titulo: 'Card 12', preco: 231 },
    { titulo: 'Card 12', preco: 231 },
    { titulo: 'Card 12', preco: 231 },
  ];

  groupedItems: any[][] = [];

  constructor() {
    this.groupItems();
  }

  groupItems() {
    this.groupedItems = [];
    
    switch (true) {
      case this.screenWidth <= 900:
        for (let i = 0; i < this.items.length; i += 3) {
          this.groupedItems.push(this.items.slice(i, i + 3));
        }
        break;
  
      case this.screenWidth >= 901 && this.screenWidth <= 1470:
        for (let i = 0; i < this.items.length; i += 5) {
          this.groupedItems.push(this.items.slice(i, i + 5));
        }
        break;
  
      default:
        for (let i = 0; i < this.items.length; i += 7) {
          this.groupedItems.push(this.items.slice(i, i + 7));
        }
        break;
    }
  }
  
  //Chama a Classe de Definir uma nova largura toda vez que a resolução mudar
  @HostListener('window:resize', ['$event'])
  onResize() {
    this.updateScreenSize();
    this.groupItems()
  }


  updateScreenSize(){
      this.screenWidth = window.innerWidth
      console.log(this.screenWidth)
  }

  ngOnInit() {
    this.updateScreenSize();
    this.groupItems()
  }
}
