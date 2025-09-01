import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarroselPhotosComponent } from './carrosel-photos.component';

describe('CarroselPhotosComponent', () => {
  let component: CarroselPhotosComponent;
  let fixture: ComponentFixture<CarroselPhotosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CarroselPhotosComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CarroselPhotosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
