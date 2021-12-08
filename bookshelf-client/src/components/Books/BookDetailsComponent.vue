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
			<SimpleSpinner v-if="showListSpinner" />
			<ButtonGroup
				:list="actualList"
				@add-to-list="addToListHandler"
				@remove-from-list="removeFromListHandler"
			/>
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
		<div class="reviews-container">
			<BookReviews :bookIssueId="book.bookIssueId" />
		</div>
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
		<div class="details-item">
			<div class="details-item__label">Publisher</div>
			<div class="details-item__content">{{ book.publisher.name }}</div>
			<div class="details-item__image">
				<FallbackImage
					:source="book.publisher.imageUrl"
					:defaultImage="defaultImage"
					:title="book.publisher.name"
					objectFit="contain"
				/>
			</div>
		</div>
	</div>
</template>

<script>
	import { ref, computed } from "vue";
	import { useStore } from "vuex";
	import defaultImage from "@/assets/images/rasters/book-placeholder.jpg";
	import FallbackImage from "@/components/Ui/Imaging/FallbackImage.vue";
	import PopupImage from "@/components/Ui/Imaging/PopupImage.vue";
	import AuthorLinks from "@/components/Author/AuthorLinks.vue";
	import RatingComponent from "@/components/Ui/StarRatingComponent.vue";
	import ButtonGroup from "@/components/Ui/Buttons/ButtonGroup.vue";
	import SimpleSpinner from "@/components/Ui/Spinners/SimpleSpinner.vue";
	import BookReviews from "@/components/Reviews/BookReviews.vue";

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
			SimpleSpinner,
			BookReviews,
		},
		setup: (props) => {
			const { state, dispatch } = useStore();
			const popupShown = ref(false);
			const showListSpinner = ref(false);

			const actualList = computed(() => (props.book.list !== "" ? props.book.list : null));
			const book = computed(() => props.book);
			const ratings = computed(() => book.value.ratings);
			const publishedOn = computed(() =>
				new Date(book.value.publishedOn).toLocaleDateString("sr")
			);
			const isSubmittingRating = computed(() => state.bookDetails.isSubmittingRating);

			const bookIssueId = book.value.bookIssueId;

			const toggleCoverPopup = () => {
				popupShown.value = !popupShown.value;
			};

			const bookRatingHandler = (value) => {
				dispatch("bookDetails/postRating", { rating: value, bookIssueId });
			};

			const addToListHandler = (listNames) => {
				showListSpinner.value = true;
				dispatch("bookDetails/addToList", { id: bookIssueId, ...listNames }).finally(() => {
					showListSpinner.value = false;
				});
			};

			const removeFromListHandler = (listName) => {
				showListSpinner.value = true;
				dispatch("bookDetails/removeFromList", { id: bookIssueId, list: listName }).finally(
					() => {
						showListSpinner.value = false;
					}
				);
			};

			return {
				actualList,
				ratings,
				defaultImage,
				publishedOn,
				popupShown,
				showListSpinner,
				toggleCoverPopup,
				bookRatingHandler,
				addToListHandler,
				removeFromListHandler,
				isSubmittingRating,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./BookDetailsComponent";
</style>
