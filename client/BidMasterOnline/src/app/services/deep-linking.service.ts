import { Injectable } from '@angular/core';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class DeepLinkingService {

  constructor(private readonly route: ActivatedRoute,
    private readonly router: Router) {

  }

  getQueryParam(key: string) {
    return this.route.snapshot.queryParams[key] || null;
  }

  getAllQueryParams() {
    return this.route.snapshot.queryParams;
  }

  async setQueryParam(key: string, value: any) {
    const params = { ...this.route.snapshot.queryParams };
    params[key] = value;

    const navigationExtras: NavigationExtras = {
      queryParams: params,
      queryParamsHandling: 'merge',
    };

    await this.router.navigate([], navigationExtras);
  }

  async clearQueryParam(key: string) {
    const params = { ...this.route.snapshot.queryParams };
    params[key] = null;

    const navigationExtras: NavigationExtras = {
      queryParams: params,
      queryParamsHandling: 'merge',
    };

    await this.router.navigate([], navigationExtras);
  }
}
