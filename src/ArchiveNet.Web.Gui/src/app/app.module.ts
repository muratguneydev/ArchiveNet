import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { TopBarComponent } from './top-bar/top-bar.component';
import { HttpCallInterceptor } from './HttpCallInterceptor';
import { ArtListComponent } from './art-list/art-list.component';
import { ArtViewComponent } from './art-view/art-view.component';
import { ArtistPageComponent } from './artist-page/artist-page.component';
import { HomePageComponent } from './home-page/home-page.component';
import { ArtEditComponent } from './art-edit/art-edit.component';
import { EditableArtViewComponent } from './editable-art-view/editable-art-view.component';
import { ArtistPhotoComponent } from './artist-photo/artist-photo.component';
import { EncryptionService } from './EncryptionService';



@NgModule({
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
	FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomePageComponent },
      { path: 'art/:artistid', component: ArtistPageComponent },
    ])
  ],
  providers: [
   // register the interceptor created
   {
	 provide: HTTP_INTERCEPTORS,
	 useClass: HttpCallInterceptor,
	 multi: true
   },
   EncryptionService
  ],
  declarations: [
    AppComponent,
    TopBarComponent,
	HomePageComponent,
	ArtListComponent,
	ArtViewComponent,
	ArtEditComponent,
	EditableArtViewComponent,
	ArtistPageComponent,
	ArtistPhotoComponent
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
