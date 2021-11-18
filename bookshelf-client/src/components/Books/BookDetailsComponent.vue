<template>
	<div class="side-container">
		<PopupImage v-if="popupShown" :image="bookCover" @dismiss="togleCoverPopup" />
		<div class="book-cover-container" @click="togleCoverPopup">
			<FallbackImage :source="bookCover" :defaultImage="defaultImage" :title="title" />
		</div>
	</div>
	<div class="main-container">
		<div class="title-large">{{ title }}</div>
		<AuthorLinks className="author-links" :authors="authors" />
		<div class="summary">{{ summary }}</div>
	</div>
</template>

<script>
	import { ref, computed, onMounted } from "vue";
	import defaultImage from "@/assets/images/rasters/book-placeholder.jpg";
	import FallbackImage from "@/components/Ui/Imaging/FallbackImage.vue";
	import PopupImage from "@/components/Ui/Imaging/PopupImage.vue";
	import AuthorLinks from "@/components/Author/AuthorLinks.vue";

	export default {
		props: {
			book: Object,
		},
		components: {
			PopupImage,
			FallbackImage,
			AuthorLinks,
		},
		setup: (props) => {
			const popupShown = ref(false);
			const book = computed(() => props.book);
			const title = book.value.title;
			const bookCover = book.value.imageUrl;
			const summary = book.value.summary;
			const authors = book.value.authors;

			console.log("Knjiga", book.value);

			onMounted(() => {
				document.title = `${title} - Book page`;
			});

			const togleCoverPopup = () => {
				popupShown.value = !popupShown.value;
			};

			return {
				bookCover,
				title,
				summary,
				authors,
				defaultImage,
				popupShown,
				togleCoverPopup,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./BookDetailsComponent";
</style>
