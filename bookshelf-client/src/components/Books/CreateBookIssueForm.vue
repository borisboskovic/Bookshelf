<template>
	<div class="card-container">
		<LoadingSpinner v-if="isSubmitting" />
		<h3>Add book issue</h3>
		<Form @submit="submitHandler">
			<div class="form-split-container">
				<div>
					<div class="form-control pt-1">
						<div class="input-label">Original title</div>
						<BookSearchComponent
							v-if="!originalTitle"
							@book-select="selectBookHandler"
						/>
						<div class="original-book__container" v-else>
							<div class="original-book__container-title">
								{{ originalTitle.title }}
							</div>
							<i class="fa fa-times fa-lg" @click="clearSelectedBook"></i>
						</div>
					</div>
					<div class="form-control pt-1">
						<div class="input-label">Publisher</div>
						<PublisherSearchComponent
							v-if="!publisher"
							@publisher-select="selectPublisherHandler"
						/>
						<div class="original-book__container" v-else>
							<div class="original-book__container-title">
								{{ publisher.name }}
							</div>
							<i class="fa fa-times fa-lg" @click="clearSelectedPublisher"></i>
						</div>
					</div>
					<div class="form-control pt-1">
						<InputField label="Title" name="title" type="text" />
					</div>
					<div class="form-control pt-1">
						<InputField label="Release date" name="releaseDate" type="date" />
					</div>
					<div class="divided-form-control">
						<div>
							<InputField
								label="Number of pages"
								name="numberOfPages"
								type="number"
							/>
						</div>
						<div>
							<InputField label="Tirage" name="tirage" type="number" />
						</div>
					</div>

					<div class="divided-form-control">
						<div>
							<InputField label="isbn" name="isbn" type="text" />
						</div>
						<div>
							<InputField label="Hardcover" name="isHardcover" type="checkbox" />
						</div>
					</div>
				</div>

				<div>
					<div class="form-control pt-1">
						<ImageUploader
							name="bookCover"
							label="Book cover"
							@wrongformat="notifyIncorrectFormat"
						/>
					</div>

					<div class="form-control pt-1 flex-grow">
						<label for="summary" class="input-label">Summary</label>
						<VeeTextArea name="summary" className="text-area__main-form" />
					</div>
				</div>
			</div>

			<div class="button-container">
				<ButtonComponent :size="'large'" :disabled="isSubmitting"> Submit </ButtonComponent>
			</div>
		</Form>
	</div>
</template>

<script ipt>
	import { ref } from "vue";
	import { Form } from "vee-validate";
	import router from "@/router";
	import { showErrorToast } from "@/services/notifications/notification-templates";
	import LoadingSpinner from "@/components/Ui/Spinners/LoadingSpinner.vue";
	import ButtonComponent from "@/components/Ui/Buttons/ButtonComponent.vue";
	import InputField from "@/components/Ui/Validation/InputField";
	import ImageUploader from "@/components/Ui/Validation/ImageUploader.vue";
	import VeeTextArea from "@/components/Ui/Validation/VeeTextArea";
	import BookSearchComponent from "../../components/Search/BookSearchComponent.vue";
	import PublisherSearchComponent from "../../components/Search/PublisherSearchComponent.vue";
	import axios from "@/config/axios";

	export default {
		components: {
			LoadingSpinner,
			Form,
			ButtonComponent,
			InputField,
			ImageUploader,
			VeeTextArea,
			BookSearchComponent,
			PublisherSearchComponent,
		},
		setup: () => {
			const isSubmitting = ref(false);
			const originalTitle = ref(null);
			const publisher = ref(null);

			const submitHandler = (values) => {
				isSubmitting.value = true;

				const formData = new FormData();
				formData.append("bookId", originalTitle.value.id);
				formData.append("publisherId", publisher.value.id);
				formData.append("title", values.title);
				formData.append("coverPhoto", values.bookCover);
				formData.append("summary", values.summary);
				formData.append("numberOfPages", values.numberOfPages);
				formData.append("tirage", values.tirage);
				formData.append("isHardcover", values.isHardcover);
				formData.append("releaseDate", values.releaseDate);
				formData.append("isbn", values.isbn);

				axios
					.post("BookDetails/CreateBookIssue", formData)
					.then((response) => {
						router.push(`/book-details/${response.data.bookIssueId}`);
					})
					.finally(() => {
						isSubmitting.value = false;
					});
			};

			const notifyIncorrectFormat = () => {
				showErrorToast("Error", "File type is not supported");
			};

			const selectBookHandler = (selectedBook) => {
				originalTitle.value = selectedBook;
			};

			const clearSelectedBook = () => {
				originalTitle.value = null;
			};

			const selectPublisherHandler = (selectedPublisher) => {
				publisher.value = selectedPublisher;
			};

			const clearSelectedPublisher = () => {
				publisher.value = null;
			};

			return {
				isSubmitting,
				submitHandler,
				notifyIncorrectFormat,
				originalTitle,
				publisher,
				selectBookHandler,
				clearSelectedBook,
				selectPublisherHandler,
				clearSelectedPublisher,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "@/assets/style/form-styles";
	@import "./CreateBookIssueForm";
</style>
