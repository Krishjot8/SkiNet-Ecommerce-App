import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckoutPaymentsComponent } from './checkout-payments.component';

describe('CheckoutPaymentsComponent', () => {
  let component: CheckoutPaymentsComponent;
  let fixture: ComponentFixture<CheckoutPaymentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CheckoutPaymentsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CheckoutPaymentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
