import { TestBed } from '@angular/core/testing';

import { CatalogServiceService } from './catalog-service.service';

describe('CatalogServiceService', () => {
  let service: CatalogServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CatalogServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
