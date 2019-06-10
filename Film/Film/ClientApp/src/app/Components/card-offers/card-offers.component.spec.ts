import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CardOffersComponent } from './card-offers.component';

describe('CardOffersComponent', () => {
  let component: CardOffersComponent;
  let fixture: ComponentFixture<CardOffersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CardOffersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CardOffersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
