import { EmptyName, Name } from "./Name";
import { EmptyNameCollection, NameCollection } from "./NameCollection";

export class Artist {
	name: Name = new Name('');
	alsoKnownAs: NameCollection = new NameCollection([]);

	constructor(name: Name, alsoKnownAs: NameCollection) {
		this.name = name;
		this.alsoKnownAs = alsoKnownAs;
	}
}

export class EmptyArtist extends Artist {
	constructor() {
		super(new EmptyName(), new EmptyNameCollection());
	}
}