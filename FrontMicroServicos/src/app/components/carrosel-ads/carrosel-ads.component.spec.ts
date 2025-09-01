import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarroselAdsComponent } from './carrosel-ads.component';

describe('CarroselAdsComponent', () => {
  let component: CarroselAdsComponent;
  let fixture: ComponentFixture<CarroselAdsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CarroselAdsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CarroselAdsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
