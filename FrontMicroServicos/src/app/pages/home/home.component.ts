import { Component } from '@angular/core';
import { HeaderComponent } from "../../components/header/header.component";
import { CardComponent } from '../../components/card/card.component';
import { CardHouseComponent } from "../../components/card-house/card-house.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeaderComponent, CardComponent, CardHouseComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
