import { Component, Input } from '@angular/core';
import { Art } from '../dto/Art';

@Component({
  selector: 'app-art-view',
  templateUrl: './art-view.component.html',
  styleUrls: ['./art-view.component.css']
})
export class ArtViewComponent {

	@Input()
	art: Art = new Art;

}
