import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
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
}

