import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarroselAdsHouseComponent } from './carrosel-ads-house.component';

describe('CarroselAdsHouseComponent', () => {
  let component: CarroselAdsHouseComponent;
  let fixture: ComponentFixture<CarroselAdsHouseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CarroselAdsHouseComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CarroselAdsHouseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
