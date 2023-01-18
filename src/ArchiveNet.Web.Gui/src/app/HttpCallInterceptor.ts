import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from './environments/environment';
import { BaseUrl } from './BaseUrl';

//https://dev.to/leonardovff/angular-consuming-multiple-apis-with-angular-httpclient-in-a-beautiful-way-using-interceptors-3im1
@Injectable()
export class HttpCallInterceptor implements HttpInterceptor {
	constructor(private baseUrl: BaseUrl) { }
	intercept(
		req: HttpRequest<any>,
		next: HttpHandler
	): Observable<HttpEvent<any>> {
		let requestUrl = req.url;
		//if (this.requestUrlHasBaseApiUrlPrefix(requestUrl)) {
			requestUrl = this.baseUrl.getRequestUrlWithReplacedApiBaseUrl(requestUrl);
		//}
		// else if (this.requestUrlHasBaseContentUrlPrefix(requestUrl)) {
		// 	requestUrl = this.getRequestUrlWithReplacedContentBaseUrl(requestUrl);
		// }
		
		req = req.clone({
			url: requestUrl
		});
		// move to next HttpClient request life cycle
		return next.handle(req);
	}

	// private getRequestUrlWithReplacedApiBaseUrl(requestUrl: string): string {
	// 	return requestUrl.replace(this.baseUrl.api, environment.baseApiUrl);
	// }

	// private requestUrlHasBaseApiUrlPrefix(requestUrl: string) {
	// 	return requestUrl.indexOf(this.baseUrl.api) !== -1;
	// }

	// private getRequestUrlWithReplacedContentBaseUrl(requestUrl: string): string {
	// 	return requestUrl.replace(this.baseUrl.content, environment.baseContentUrl);
	// }

	// private requestUrlHasBaseContentUrlPrefix(requestUrl: string) {
	// 	return requestUrl.indexOf(this.baseUrl.content) !== -1;
	// }
}

