<template>
	<div class="side-container">
		<PopupImage v-if="popupShown" :image="book.imageUrl" @dismiss="toggleCoverPopup" />
		<div class="book-cover-container" @click="toggleCoverPopup">
			<FallbackImage
				:source="book.imageUrl"
				:defaultImage="defaultImage"
				:title="book.title"
			/>
		</div>
		<div class="book-details__button-group">
			<ButtonGroup :list="book.list" />
		</div>
		<RatingComponent
			:average="ratings.average"
			:yourRating="ratings.yourRating"
			:isSubmitting="isSubmittingRating"
			@rating="bookRatingHandler"
		/>
	</div>

	<div class="main-container">
		<div class="title-large">{{ book.title }}</div>
		<AuthorLinks className="author-links" :authors="book.authors" />
		<div class="summary">{{ book.summary }}</div>
	</div>

	<div class="details-container">
		<div class="details-item">
			<div class="details-item__label">Published on</div>
			<div class="details-item__content">{{ publishedOn }}</div>
		</div>
		<div class="details-item">
			<div class="details-item__label">Original published on</div>
			<div class="details-item__content">{{ book.originalPublishedOn }}</div>
		</div>
		<div class="details-item">
			<div class="details-item__label">Original title</div>
			<div class="details-item__content">{{ book.originalTitle }}</div>
		</div>
		<div class="details-item">
			<div class="details-item__label">ISBN</div>
			<div class="details-item__content">{{ book.ISBN13 ?? book.ISBN }}</div>
		</div>
		<div class="details-item">
			<div class="details-item__label">Language</div>
			<div class="details-item__content">{{ book.language }}</div>
		</div>
	</div>
</template>

<script>
	import { ref, computed, onMounted } from "vue";
	import { useStore } from "vuex";
	import defaultImage from "@/assets/images/rasters/book-placeholder.jpg";
	import FallbackImage from "@/components/Ui/Imaging/FallbackImage.vue";
	import PopupImage from "@/components/Ui/Imaging/PopupImage.vue";
	import AuthorLinks from "@/components/Author/AuthorLinks.vue";
	import RatingComponent from "@/components/Ui/StarRatingComponent.vue";
	import ButtonGroup from "@/components/Ui/Buttons/ButtonGroup.vue";

	export default {
		props: {
			book: Object,
		},
		components: {
			PopupImage,
			FallbackImage,
			AuthorLinks,
			RatingComponent,
			ButtonGroup,
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

			const isSubmittingRating = computed(() => state.bookDetails.isSubmittingRating);

			console.log("Knjiga", book.value);

			onMounted(() => {
				document.title = `${book.value.title} - Book page`;
			});

			const toggleCoverPopup = () => {
				popupShown.value = !popupShown.value;
			};

			const bookRatingHandler = (value) => {
				dispatch("bookDetails/postRating", { rating: value, bookIssueId });
			};

			return {
				ratings,
				defaultImage,
				publishedOn,
				popupShown,
				toggleCoverPopup,
				bookRatingHandler,
				isSubmittingRating,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./BookDetailsComponent";
</style>
