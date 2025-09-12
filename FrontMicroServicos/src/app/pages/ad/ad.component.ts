import { Component } from '@angular/core';
import { HeaderComponent } from '../../components/header/header.component';
import { CarroselPhotosComponent } from '../../components/carrosel-photos/carrosel-photos.component';

@Component({
  selector: 'app-ad',
  standalone: true,
  imports: [HeaderComponent, CarroselPhotosComponent],
  templateUrl: './ad.component.html',
  styleUrl: './ad.component.scss'
})
export class AdComponent {

}
