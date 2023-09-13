import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViajeComponent } from './viaje.component';

describe('ViajeComponent', () => {
  let component: ViajeComponent;
  let fixture: ComponentFixture<ViajeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViajeComponent]
    });
    fixture = TestBed.createComponent(ViajeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
