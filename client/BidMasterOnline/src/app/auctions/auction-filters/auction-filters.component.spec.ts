import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuctionFiltersComponent } from './auction-filters.component';

describe('AuctionFiltersComponent', () => {
  let component: AuctionFiltersComponent;
  let fixture: ComponentFixture<AuctionFiltersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuctionFiltersComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AuctionFiltersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
