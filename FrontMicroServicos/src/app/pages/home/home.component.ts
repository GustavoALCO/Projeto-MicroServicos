import { Component } from '@angular/core';
import { HeaderComponent } from "../../components/header/header.component";
import { CardComponent } from '../../components/card/card.component';
import { CardHouseComponent } from "../../components/card-house/card-house.component";
import { CarroselPhotosComponent } from "../../components/carrosel-photos/carrosel-photos.component";
import { CarroselAdsComponent } from "../../components/carrosel-ads/carrosel-ads.component";
import { CarroselAdsHouseComponent } from "../../components/carrosel-ads-house/carrosel-ads-house.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeaderComponent, CardComponent, CardHouseComponent, CarroselPhotosComponent, CarroselAdsComponent, CarroselAdsHouseComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
image: string[] = 
[
  '../../../assets/Images/imagemGPTCarroAjustado.webp',
  '../../../assets/Images/imagemGPTCasaAjustado.webp' 
]
}
