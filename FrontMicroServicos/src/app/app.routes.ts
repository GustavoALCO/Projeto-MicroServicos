import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { AdComponent } from './pages/ad/ad.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'cars', component:  AdComponent}
    
];
