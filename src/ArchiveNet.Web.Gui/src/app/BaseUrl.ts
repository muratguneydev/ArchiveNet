import { Injectable } from '@angular/core';
import { environment } from './environments/environment';

@Injectable({
	providedIn: 'root'
})
export class BaseUrl {
	
	api: string = '@api-art';
	//content: string = '@content';
	//artistImage: string = '@content/Artist/Images';
	artistImage: string = `${environment.baseContentUrl}/Artist/Images`;

	public getRequestUrlWithReplacedApiBaseUrl(requestUrl: string): string {
		return requestUrl.replace(this.api, environment.baseApiUrl);
	}

	// public getRequestUrlWithReplacedContentBaseUrl(requestUrl: string): string {
	// 	return requestUrl.replace(this.content, environment.baseContentUrl);
	// }

	// private requestUrlHasBaseApiUrlPrefix(requestUrl: string) {
	// 	return requestUrl.indexOf(this.api) !== -1;
	// }

	// private requestUrlHasBaseContentUrlPrefix(requestUrl: string) {
	// 	return requestUrl.indexOf(this.content) !== -1;
	// }
}
