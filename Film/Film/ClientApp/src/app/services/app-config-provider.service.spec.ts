import { TestBed } from '@angular/core/testing';

import { AppConfigProviderService } from './app-config-provider.service';

describe('AppConfigProviderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AppConfigProviderService = TestBed.get(AppConfigProviderService);
    expect(service).toBeTruthy();
  });
});
