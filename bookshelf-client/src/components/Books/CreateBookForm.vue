<template>
	<div class="card-container">
		<LoadingSpinner v-if="isSubmitting" />
		<h3>Add book</h3>
		<Form @submit="submitHandler">
			<div class="form-split-container">
				<div>
					<div class="form-control pt-1">
						<InputField label="Original title" name="originalTitle" type="text" />
					</div>
					<div class="form-control pt-1">
						<InputField label="Original release date" name="releaseDate" type="date" />
					</div>
				</div>
				<div>
					<div class="form-control pt-1 input-label">Authors</div>
					<AuthorSearchComponent @author-select="addAuthorHandler" />
					<div v-for="author in authors" :key="author.id" class="author-item">
						<div class="author-item__name">{{ author.name }}</div>
						<i
							class="author_item__remove-icon fa fa-times fa-lg"
							@click="() => removeAuthorHandler(author.id)"
						/>
					</div>
				</div>
			</div>
			<div class="button-container">
				<ButtonComponent :size="'large'" :disabled="isSubmitting"> Submit </ButtonComponent>
			</div>
		</Form>
	</div>
</template>

<script>
	import { ref } from "vue";
	import { Form } from "vee-validate";
	import LoadingSpinner from "@/components/Ui/Spinners/LoadingSpinner.vue";
	import ButtonComponent from "../../components/Ui/Buttons/ButtonComponent.vue";
	import InputField from "@/components/Ui/Validation/InputField";
	import AuthorSearchComponent from "../../components/Search/AuthorSearchComponent.vue";

	export default {
		components: {
			LoadingSpinner,
			Form,
			ButtonComponent,
			InputField,
			AuthorSearchComponent,
		},
		setup: () => {
			const isSubmitting = ref(false);
			const authors = ref([
				{
					id: 1,
					name: "Ljubivoje ršumović",
				},
				{
					id: 2,
					name: "Branko Ćopić",
				},
			]);

			const submitHandler = (values) => {
				const authorIds = authors.value.map((e) => e.id);
				const body = {
					...values,
					authorIds,
				};
			};

			const removeAuthorHandler = (id) => {
				authors.value = authors.value.filter((e) => e.id !== id);
			};

			const addAuthorHandler = (author) => {
				if (!authors.value.find((e) => e.id === author.id)) {
					authors.value = authors.value.concat(author);
				}
			};

			return {
				isSubmitting,
				submitHandler,
				authors,
				removeAuthorHandler,
				addAuthorHandler,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "@/assets/style/form-styles";
	@import "./CreateBookForm";
</style>
