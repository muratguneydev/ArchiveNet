//Generated by using: "ng generate directive ApplyDefaultArtistImage"
//https://angular.io/guide/attribute-directives
import { Directive, ElementRef, HostListener } from '@angular/core';
import { BaseUrl } from './BaseUrl';

@Directive({
  selector: 'img[appApplyDefaultArtistImage]'
})
export class ApplyDefaultArtistImageDirective {

  constructor(private el: ElementRef) { }

  @HostListener('error')
  public onError() {
	  //console.log('Couldnt find image...');
	  //this.el.nativeElement.style.display = "none";
	  //this.el.nativeElement.src = `${this.baseUrl.artistImage}/Default.png`;
	  this.el.nativeElement.src = `assets/Default.png`;
  }

}
