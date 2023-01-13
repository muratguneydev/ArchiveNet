import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from './environments/environment';

//https://dev.to/leonardovff/angular-consuming-multiple-apis-with-angular-httpclient-in-a-beautiful-way-using-interceptors-3im1
@Injectable()
export class HttpCallInterceptor implements HttpInterceptor {
	readonly BaseApiUrlStringToReplace = '@api-art';
	constructor() { }
	intercept(
		req: HttpRequest<any>,
		next: HttpHandler
	): Observable<HttpEvent<any>> {
		let requestUrl = req.url;
		if (this.requestUrlHasBaseApiUrlPrefix(requestUrl)) {
			requestUrl = this.getRequestUrlWithReplacedBaseUrl(requestUrl);
		}
		
		req = req.clone({
			url: requestUrl
		});
		// move to next HttpClient request life cycle
		return next.handle(req);
	}

	private getRequestUrlWithReplacedBaseUrl(requestUrl: string): string {
		return requestUrl.replace(this.BaseApiUrlStringToReplace, environment.baseApiUrl);
	}

	private requestUrlHasBaseApiUrlPrefix(requestUrl: string) {
		return requestUrl.indexOf(this.BaseApiUrlStringToReplace) !== -1;
	}
}