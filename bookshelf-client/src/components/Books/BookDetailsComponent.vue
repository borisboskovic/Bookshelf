<template>
	<div class="side-container">
		<PopupImage v-if="popupShown" :image="bookCover" @dismiss="togleCoverPopup" />
		<div class="book-cover-container" @click="togleCoverPopup">
			<FallbackImage :source="bookCover" :defaultImage="defaultImage" :title="title" />
		</div>
		<RatingComponent
			:average="ratings.average"
			:yourRating="ratings.yourRating"
			:isSubmitting="isSubmittingRating"
			@rating="bookRatingHandler"
		/>
		<div class="publication-dates">
			<div>Published on</div>
			<div class="date">{{ publishedOn }}</div>
		</div>
		<div class="original-title">
			<div>Original title</div>
			<div class="title">{{ book.originalTitle }}</div>
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
	import { useStore } from "vuex";
	import defaultImage from "@/assets/images/rasters/book-placeholder.jpg";
	import FallbackImage from "@/components/Ui/Imaging/FallbackImage.vue";
	import PopupImage from "@/components/Ui/Imaging/PopupImage.vue";
	import AuthorLinks from "@/components/Author/AuthorLinks.vue";
	import RatingComponent from "../../components/Ui/StarRatingComponent.vue";

	export default {
		props: {
			book: Object,
		},
		components: {
			PopupImage,
			FallbackImage,
			AuthorLinks,
			RatingComponent,
		},
		setup: (props) => {
			const { state, dispatch } = useStore();
			const popupShown = ref(false);
			const book = computed(() => props.book);

			const ratings = computed(() => book.value.ratings);
			const publishedOn = computed(() =>
				new Date(book.value.publishedOn).toLocaleDateString("sr")
			);

			const bookIssueId = book.value.bookIssueId;
			const title = book.value.title;
			const bookCover = book.value.imageUrl;
			const summary = book.value.summary;
			const authors = book.value.authors;

			const isSubmittingRating = computed(() => state.bookDetails.isSubmittingRating);

			console.log("Knjiga", book.value);

			onMounted(() => {
				document.title = `${title} - Book page`;
			});

			const togleCoverPopup = () => {
				popupShown.value = !popupShown.value;
			};

			const bookRatingHandler = (value) => {
				dispatch("bookDetails/postRating", { rating: value, bookIssueId });
			};

			return {
				bookCover,
				title,
				summary,
				authors,
				ratings,
				defaultImage,
				publishedOn,
				popupShown,
				togleCoverPopup,
				bookRatingHandler,
				isSubmittingRating,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./BookDetailsComponent";
</style>
